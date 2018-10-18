import React from "react"
import { Route } from 'react-router-dom'

import Rightbar from '../components/bar/Rightbar'
import Leftbar from '../components/bar/Leftbar'
import Headerbar from '../components/bar/Headerbar'
import Footerbar from '../components/bar/Footerbar'

import Sidemenu from '../components/Sidemenu'



/*
 * [Default layout for most of the routes. Will render navbar and footer (if any).]
 * @param {[Class]} component [Which component will be rendered.]
 * @param {[Props]} rest      [Rest of the props (if any) except component.]
 */
export const DefaultLayout = ({
  component: Component,
  ...rest
}) => {
  return (<Route {...rest} render={matchProps => (<div className="DefaultLayout">
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



/*
 * [Login layout for login page. same as default but without nav and footer (if any)]
 * @param {[Class]} component [Which component will be rendered.]
 * @param {[Props]} rest      [Rest of the props (if any) except component.]
 */
export const LoginLayout = ({
  component: Component,
  ...rest
}) => {
  return (<Route {...rest} render={matchProps => (<div className="LoginLayout">
      <Component {...matchProps}/>
    </div>)}/>)
};



/*
 * [ACCOUNT layout for the account routes. Will render navbar and footer (if any). Also sidebar.]
 * @param {[Class]} component [Which component will be rendered.]
 * @param {[Props]} rest      [Rest of the props (if any) except component.]
 */
export const AccountLayout = ({
  component: Component,
  ...rest
}) => {
  return (<Route {...rest} render={matchProps => (
    <div className="account-layout">
      <headerbar/>
      <div className="account-overview">
        <Sidemenu/>
        <Component {...matchProps}/>
      </div>
    </div>)}/>)
};
