import React, {Component} from 'react';
import SideMenuItem from './SideMenuItem'

const sideMenu = {
  minHeight: '100vh',
  backgroundColor: '#242323',
  width: '300px',
}

class Sidemenu extends Component {
  render() {
    return (
      <div style={sideMenu} className="side-menu">
        <nav className="account-nav">
          <SideMenuItem extra="exact" para="Översikt" nav="/account" icon="fas fa-pencil-alt" />
          <SideMenuItem para="Betalningstjänster" nav="/account/accounting" icon="fas fa-dollar-sign"/>
          <SideMenuItem para="Bokföringstjänst" nav="/account/invoices" icon="fas fa-book" />
        </nav>
      </div>
    );
  }
}

export default Sidemenu;
