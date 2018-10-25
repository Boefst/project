import React from "react"
import { Route } from 'react-router-dom'

import Rightbar from '../components/bar/rightbar/Rightbar'
import Leftbar from '../components/bar/leftbar/Leftbar'
import Headerbar from '../components/bar/headerbar/Headerbar'
import Footerbar from '../components/bar/footerbar/Footerbar'

export const GameLayout = ({
  component: Component,
  ...rest
}) => {
  return (<Route {...rest} render={matchProps => (
    <div className="DefaultLayout">
      <div className="body-wrapper">
        <div className="headerbar-wrapper">
          <Headerbar />
        </div>
        <div className="middle">
          <div className="leftbar-container">
            <Leftbar />
          </div>
          <div className="main-container">
            <div className="content">
              <Component {...matchProps}/>
            </div>
          </div>
          <div className="rightbar-container">
            <Rightbar />
          </div>
        </div>
        <div className="footerbar-wrapper">
          <Footerbar />
        </div>
      </div>
    </div>)}/>)
};

export const LoginLayout = ({
  component: Component,
  ...rest
}) => {
  return (<Route {...rest} render={matchProps => (
    <div className="LoginLayout">
      <Component {...matchProps}/>
    </div>)}/>)
};
