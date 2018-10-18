import React, {Component} from 'react'
import { NavLink, Link, withRouter } from 'react-router-dom'
import { connect } from 'react-redux'

class Footerbar extends Component {

  render() {
    return (
      <div className="asd">
        <p>Footerbar</p>
      </div>
    )
  }
}

export default withRouter(connect((store) => {
    return {
    }
})(Footerbar));
