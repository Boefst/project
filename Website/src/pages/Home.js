import React, { Component } from 'react'
import  { connect } from 'react-redux'
import { Redirect } from 'react-router-dom'

import Login from '../components/home/Login'

class Home extends Component {
  constructor(props){
    super()
  }

  render() {
    if (this.props.loggedIn === true) {
      return <Redirect to="/overview" />
    }else{
      return(
        <div className="home-body">
          <h1>Kinship</h1>
          <Login />
        </div>
      );
    }
  }
}

export default connect((store) => {
  return {
    loggedIn: store.login.loggedIn
  }
})(Home);
