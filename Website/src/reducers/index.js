import { combineReducers } from 'redux'

import auth from './authReducer'
import login from './loginReducer'

export default combineReducers({
  auth,
  login
})
