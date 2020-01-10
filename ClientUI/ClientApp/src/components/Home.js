import React, { Component } from 'react';

export class Home extends Component {
  constructor(props) {
    super(props);
    this.state = { message: "" };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  //async sendMessage(message) {
  //  //const response = await fetch('home/Submit?id="123"');
  //  const response = await fetch('home/Submit');

  //  const data = await response.json();

  //}

  handleChange(event) {
    this.setState({ value: event.target.message });
  }

  async handleSubmit(event) {
    const response = await fetch('home/Submit');
    const data = await response.json();
    alert('A message was submitted: ' + this.state.message);
    event.preventDefault();
  }

  render() {

    return (
      <div>
        <form onSubmit={this.handleSubmit}>
          <label className="form-control">
            Message:
          <input className="form-control" type="text" value={this.state.message} onChange={this.handleChange} />
          </label>
          <input className="btn btn-circle btn-success" type="submit" value="Submit" />
        </form>
      </div>
    );
  }
}
