import React, { Component } from 'react';

export class Messages extends Component {
  static displayName = Messages.name;

  constructor(props) {
    super(props);
    this.state = { messages: [], loading: true, message: "", model: {} };

    //function binding operation
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentDidMount() {
    this.messagesData();
  }

  handleChange(event) {
    this.setState({ message: event.target.value });
  }

  async handleSubmit(event) {
    event.preventDefault();
    const response = await fetch('https://localhost:44381/api/servicebus/Submit/?message=' + this.state.message);
    const data = await response.json();
    //alert('A message was submitted: ' + this.state.message);
    this.setState({ model: data });
  }

  //async sendMessage(message) {
  //  const response = await fetch('https://localhost:44381/api/servicebus/Submit/?message=' + message);

  //  const data = await response.json();
  //  this.setState({ model: data });
  //}

  static renderMessages(messages) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>User</th>
            <th>Content</th>
            <th>Datetime</th>
          </tr>
        </thead>
        <tbody>
          {messages.map(message =>
            <tr key={message.id}>
              <td>{message.messageUser}</td>
              <td>{message.messageContent}</td>
              <td>{message.messageDate}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Messages.renderMessages(this.state.messages);

    return (

      <div className="container">

        <div className="row">
          <h1 id="tabelLabel" >Messages</h1>
          {contents}
        </div>

        <form className="form-horizontal" onSubmit={this.handleSubmit}>

          <div className="form-group">
            <span className="col-md-1 col-md-offset-2 text-center"><i className="fa fa-pencil-square-o bigicon"></i></span>
            <div className="col-md-8">
              <textarea className="form-control" id="message" name="message" value={this.state.message} onChange={this.handleChange} rows="7"></textarea>
            </div>
          </div>
          <div className="form-group">
            <div className="col-md-12 text-center">
              <button type="submit" className="btn btn-circle btn-success btn-lg">Send Message</button>
            </div>
          </div>

        </form>
      </div>

      
    );
  }

  async messagesData() {
    const response = await fetch('https://localhost:44381/api/servicebus/GetMessages');
    const data = await response.json();
    this.setState({ messages: data, loading: false });
  }
}
