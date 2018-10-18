import React, {Component} from 'react'
import { NavLink, Link, withRouter } from 'react-router-dom'
import { connect } from 'react-redux'

class Headerbar extends Component {

  render() {
    return (
      <div className="asd">
        <p>Headerbar</p>
      </div>
    )
  }
}

export default withRouter(connect((store) => {
    return {
    }
})(Headerbar));
