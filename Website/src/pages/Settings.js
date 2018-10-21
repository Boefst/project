import React, { Component } from 'react'
import  { connect } from 'react-redux'
import { Redirect } from 'react-router-dom'

import { checkAuth } from '../actions/authAction'

class Settings extends Component {
  constructor(props){
    super()
  }

  async componentWillMount(){
    await this.props.dispatch(checkAuth(sessionStorage.getItem('clientID'), sessionStorage.getItem('clientSecret')));
  }

  render() {
    if (this.props.loggedIn === true) {
      return(
        <div>
          <p>Settings</p>
        </div>
      );
    } else if (this.props.loggedIn === false) {
        return <Redirect to="/home" />
    } else if (this.props.loggedIn === null) {
        return <p></p>
    }
  }
}

export default connect((store) => {
  return {
    loggedIn: store.auth.authVerified
  }
})(Settings);
