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
    const response = await fetch('home/Submit');
    const data = await response.json();
    alert('A message was submitted: ' + this.state.message);
    this.setState({ model: data });
  }

  render() {

    return (
      <div>
        <form onSubmit={this.handleSubmit}>
          <label>
            Message:
          <input className="form-control" type="text" value={this.state.message} onChange={this.handleChange} />
          </label>
          <input className="btn btn-circle btn-success" type="submit" value="Submit" />
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