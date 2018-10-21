export default function reducer(state={
  auth: {
    clientID : null,
    clientSecret: null,
  },
  authVerified: null
}, action) {

  switch (action.type) {
    case 'ADD_AUTH': {
      return {
        ...state,
        auth: {...state.auth,
          clientID : action.payload.clientID,
          clientSecret: action.payload.clientSecret,
        }
      }
    }
    case 'AUTH_DO': {
      return { ...state, authVerified: action.payload}
    }
    default: {
      return { ...state, error: 'default'}
    }
  }
}
