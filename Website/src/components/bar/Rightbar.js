import React, {Component} from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'

class Rightbar extends Component {

  render() {
    return (
      <div className="asd">
        <p>Rightbar</p>
      </div>
    )
  }
}

export default withRouter(connect((store) => {
    return {
    }
})(Rightbar));
