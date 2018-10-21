import React, {Component} from 'react'
import { NavLink, withRouter } from 'react-router-dom'
import { connect } from 'react-redux'

import LeftAccount from './leftbarComps/LeftAccount'

class Leftbar extends Component {

  render() {
    var path = this.props.location.pathname;
    if(path.includes('/account')){
      return (
        <LeftAccount />
      )
    }
    else{
      return(
        <p>Leftbar</p>
      )
    }
  }
}

export default withRouter(connect((store) => {
    return {
    }
})(Leftbar));
