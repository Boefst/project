import React, { Component } from 'react'
import  { connect } from 'react-redux'
import { Redirect } from 'react-router-dom'

class Home extends Component {
  constructor(props){
    super()
    this.state = {}
  }

  async componentWillMount(){
    //await this.props.dispatch(checkAuth(sessionStorage.getItem('clientID'), sessionStorage.getItem('clientSecret')));
  }

  render() {
    return(
      <div>
        <p>Home</p>
      </div>
    );
  }
}

export default connect((store) => {
  return {
  }
})(Home);
