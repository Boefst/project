import React, {Component} from 'react';
import { NavLink } from 'react-router-dom';

const accountA = {
  display: 'flex',
  flexDirection: 'row',
  alignItems: 'center',
  color: '#ececec',
  textDecoration: 'none',
  padding: '4% 0',
}

const icon = {
  fontSize: '1.2em',
  margin: '2%',
  color: '#ececec',
  flex: '1',
}

const p = {
  flex: '3',
}

class SideMenuItem extends Component {
  render() {
    return (
      <NavLink exact activeClassName="nav-item-active" to={this.props.nav} style={accountA} className="nav-item">
        <i style={icon} className={this.props.icon}></i>
        <p style={p}>{this.props.para}</p>
      </NavLink>
    );
  }
}

export default SideMenuItem;
