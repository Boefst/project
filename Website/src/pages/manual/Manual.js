import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Redirect } from 'react-router-dom'

import Unmatched from '../../components/manual/Unmatched'

class Manual extends Component {
    constructor(props) {
        super()
    }

    async componentWillMount() {
        //await this.props.dispatch(checkAuth(sessionStorage.getItem('clientID'), sessionStorage.getItem('clientSecret')));
    }

    render() {
      return (
          <Unmatched />
      );
    }
}

export default connect((store) => {
    return {
    }
})(Manual);
