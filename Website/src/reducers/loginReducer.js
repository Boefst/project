export default function reducer(state={
  loginInfo: {
    username: "null",
    password: "null",
  },
  loggedIn: null,
  infoMessage: null,
  isAdminUser: null
}, action) {

  switch (action.type) {
    case 'ADD_INFO': {
      return {
        ...state,
        loginInfo: {...state.loginInfo,
          username: action.payload.username,
          password: action.payload.password,
        }
      }
    }

    case 'LOGGED_IN_VERIFIED': {
      return { ...state, loggedIn: true, infoMessage: null}
    }
    case 'LOGGED_IN_DENIED': {
      return { ...state, loggedIn: false, infoMessage: "Fel användarnamn eller lösenord"}
    }
    case 'LOGGED_IN_ERROR': {
      return { ...state, loggedIn: false, infoMessage: "Server error"}
    }
    case 'LOGGED_IN_NULL': {
      return { ...state, loggedIn: null}
    }
    case 'LOGIN_RESET_STATE': {
      return {loggedIn: null, infoMessage: null}
    }
    default: {
      return { ...state, error: 'default'}
    }
  }
}
