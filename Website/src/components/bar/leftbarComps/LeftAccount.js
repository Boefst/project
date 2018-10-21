import React, {Component} from 'react'
import { NavLink, withRouter } from 'react-router-dom'
import { connect } from 'react-redux'

class LeftAccount extends Component {

  render() {
    return (
      <div className="left-links">
        <h3>Account</h3>
        <button className="leftbar-button"><NavLink to="/account/profile" className="leftbar-link">Profile</NavLink></button>
        <button className="leftbar-button"><NavLink to="/account/settings" className="leftbar-link">Account Settings</NavLink></button>
        <button className="leftbar-button"><NavLink to="/account/achievements" className="leftbar-link">Achievements</NavLink></button>
        <button className="leftbar-button"><NavLink to="/account/friends" className="leftbar-link">Friends</NavLink></button>
      </div>
    )
  }
}

export default withRouter(connect((store) => {
    return {
    }
})(LeftAccount));
