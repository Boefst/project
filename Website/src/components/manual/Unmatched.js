import React, { Component } from 'react'
import { connect } from 'react-redux'

import UnmatchedInvoices from './UnmatchedInvoices'

import { getUnmatched } from '../../actions/getUnmatchedAction'

class Unmatched extends Component {
    constructor(props) {
        super()
    }

    async componentWillMount(){
      await this.props.dispatch(getUnmatched());
    }

    render() {
        return (
            <div className="unmatchedWrapper">
                <div className="matchHeader">
                    <h2 className="wrapper-h2">Bokför manuellt</h2>
                </div>
                <div className="tableWrapper">
                    <UnmatchedInvoices />
                </div>
            </div>
        );
    }
}

export default connect((store) => {
    return {
    }
})(Unmatched);
