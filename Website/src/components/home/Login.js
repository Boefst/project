import React, {Component} from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'

import { loginUser } from '../../actions/loginAction'

const p = {
  color: '#e96c56'
}

class Login extends Component {
  constructor(props){
    super()
  this.state = {
      username: "",
      password: "",
      submitted: false
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event){
    let change = {}
    change[event.target.name] = event.target.value;
    this.setState(change);
  }

  handleSubmit(event){
   (async () => {
     await this.props.dispatch(loginUser("Boefst", "hejsan123"));
     //await this.props.dispatch(loginUser(this.state.username, this.state.password));
   })()
   event.preventDefault();
  }

  render() {
    return (
      <div className="login-container">
        <h3>Log in</h3>
        <form className="login-box" onSubmit={this.handleSubmit}>
            <input name="username" value={this.state.username} onChange={this.handleChange} type="text" className="login-input" placeholder="Username" required></input>
            <br></br>
            <input name="password" value={this.state.password} onChange={this.handleChange} type="password" className="login-input" placeholder="Password" required></input>
            {this.props.infoM !== null && (
                <p style={p}>{this.props.infoM}</p>
            )}
            <div className="lower">
                <input className="login-button" type="submit" value="Logga in" />
                <br></br>
                <a className="passreset" href="/passwordreset">Password recovery</a>
            </div>
        </form>
      </div>
    )
  }
}

export default withRouter(connect((store) => {
    return {
      infoM: store.login.infoMessage,
      loggedIn: store.login.loggedIn
    }
})(Login));
