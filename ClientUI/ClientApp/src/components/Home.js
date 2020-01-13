import React, { Component } from 'react';

export class Home extends Component {
  constructor(props) {
    super(props);
    this.state = {
      message: "", model: {} };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.sendMessage = this.sendMessage.bind(this);
  }

  async sendMessage(message) {
    const response = await fetch('https://localhost:44381/api/servicebus/Submit/?message=' + message);
    //const response = await fetch('https://localhost:44381/home/test');

    const data = await response.json();
    this.setState({ model: data});
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

  render() {

    return (

      <div className="container">

        <p> { this.state.model.Message } </p>

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
}
//<button className="btn btn-circle btn-success" onClick={this.sendMessage}> Send Message </button> 
/*
<div className="form-group">
  <label>
    Message:
              <input className="form-control" type="text" value={this.state.message} onChange={this.handleChange} />
  </label>
  <input className="btn btn-circle btn-success form-control" type="submit" value="Submit" />
</div>
*/