import { apiAddress } from './apiName'



/**
 * [add user to redux]
 * @param {[type]} name [username]
 * @param {[type]} pass [password]
 */
export function addUser(name, pass) {
  return {
    type: 'ADD_INFO',
    payload: {
      username: name,
      password: pass,
    }
  }
}
/**
 * [reset values, for login, so that if user does not reload page, the redux variables will reset]
 */
export function resetValues(){
  return {type:'LOGGED_IN_NULL'}
}

/**
 * [logout functionallity]
 * @param  {Boolean} isAdminUser [checks if the user is admin or not]
 * @return {[type]}              [redux reducer]
 */
export function logout(isAdminUser) {
    return function (dispatch) {
        dispatch({ type: 'AUTH_DO', payload: null });
        const request = async () => {
            try {
                const response = await fetch(apiAddress + '/api/users/logout', {
                    mode: 'cors',
                    headers: {
                        'content-type': 'application/json',
                        'Access-Control-Allow-Origin': '*',
                        'ClientID': sessionStorage.getItem('clientID'),
                        'ClientSecret': sessionStorage.getItem('clientSecret'),
                    },
                    method: 'GET',
                });

                const json = await response.json();
                if (json.code === 200) {
                    sessionStorage.clear();
                }
                else {

                }
            } catch (e) {

            }
        }
        request();
    }
}



/**
 * [Login as user]
 * @param  {[type]} user [username]
 * @param  {[type]} pass [password]
 * @return {[type]}      [redux reducer]
 */
export function loginUser(user, pass) {
  return function(dispatch) {
    let data = {
      "Username": user,
      "Password": pass,
      "Ip-address": "127.0.0.1"
    };
    const request = async () => {
      try {
        await dispatch({ type: 'LOGGED_IN_NULL' })
        const response = await fetch( apiAddress + '/api/account/login', {
          method: 'POST',
          mode: 'cors',
          headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
          },
          body: JSON.stringify(data),
        });
        const json = await response.json();
        if (json.Status === 200) {
          await sessionStorage.setItem('clientID', json.Data.ClientID);
          await sessionStorage.setItem('clientSecret', json.Data.ClientSecret);
          await sessionStorage.setItem('userID', json.Data.UserID);
          await dispatch({ type: 'AUTH_DO', payload: true });
          await dispatch({ type: 'LOGGED_IN_VERIFIED' });
        }
        else if (json.Status === 401){
          await dispatch({ type: 'LOGGED_IN_DENIED'})
          await dispatch({ type: 'LOGGED_IN_NULL'})
        }
        else{
          await dispatch({ type: 'LOGGED_IN_ERROR'})
          await dispatch({ type: 'LOGGED_IN_NULL'})
        }
      } catch (e) {
        await dispatch({ type: 'LOGGED_IN_ERROR'})
        await dispatch({ type: 'LOGGED_IN_NULL'})
      }
    }
    request();
  }
}
