import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux'
import store from './store';
import registerServiceWorker from './registerServiceWorker';
import {
  BrowserRouter as Router,
  Switch,
  Redirect,
} from 'react-router-dom'

import { GameLayout, LoginLayout } from "./pages/Layout";
import Home from "./pages/Home"
import Overview from "./pages/Overview"
import Profile from "./pages/Profile"
import Settings from "./pages/Settings"
import Achievements from "./pages/Achievements"
import Friends from "./pages/Friends"
import Support from "./pages/Support"
import Help from "./pages/Help"
import Forum from "./pages/Forum"

ReactDOM.render(
  <Provider store={store}>
    <Router>
      <Switch>
        <Redirect exact from="/" to="/home" />
        <LoginLayout path="/home" component={Home} />
        <GameLayout path="/overview" component={Overview} />
        <GameLayout path="/account/profile" component={Profile} />
        <GameLayout path="/account/settings" component={Settings} />
        <GameLayout path="/account/achievements" component={Achievements} />
        <GameLayout path="/account/friends" component={Friends} />
        <GameLayout path="/support" component={Support} />
        <GameLayout path="/help" component={Help} />
        <GameLayout path="/forum" component={Forum} />
      </Switch>
    </Router>
  </Provider>,
  document.getElementById('root')
);
registerServiceWorker();
