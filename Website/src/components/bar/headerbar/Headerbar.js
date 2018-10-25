import React, {Component} from 'react'
import { NavLink, withRouter } from 'react-router-dom'
import { connect } from 'react-redux'

class Headerbar extends Component {

  render() {
    return (
      <div className="header-style">
        <button className="header-button"><NavLink to="/overview" className="header-link">Overview</NavLink></button>
        <button className="header-button"><NavLink to="/account/profile" className="header-link">Account</NavLink></button>
      </div>
    )
  }
}

export default withRouter(connect((store) => {
    return {
    }
})(Headerbar));
