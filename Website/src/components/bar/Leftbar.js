import React, {Component} from 'react'
import { NavLink, Link, withRouter } from 'react-router-dom'
import { connect } from 'react-redux'

class Leftbar extends Component {

  render() {
    return (
      <div className="asd">
        <p>Leftbar</p>
      </div>
    )
  }
}

export default withRouter(connect((store) => {
    return {
    }
})(Leftbar));
