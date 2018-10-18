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

import { DefaultLayout } from "./pages/Layout";
import Home from "./pages/Home"

ReactDOM.render(
  <Provider store={store}>
    <Router>
      <Switch>
        <Redirect exact from="/" to="/home" />
        <DefaultLayout path="/home" component={Home} />
      </Switch>
    </Router>
  </Provider>,
  document.getElementById('root')
);
registerServiceWorker();
