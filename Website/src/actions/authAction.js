import { apiAddress } from './apiName'

export function checkAuth(id, secret){
    return function (dispatch) {
    dispatch({ type: 'AUTH_DO', payload: null });
    const request = async () => {
      try {
        const response = await fetch( apiAddress + '/api/account/status', {
          method: 'GET',
          mode: 'cors',
          headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'ClientID': id,
            'ClientSecret': secret,
          },
        });
        const json = await response.json();

        if (json.Status === 200) {
            await dispatch({ type: 'AUTH_DO', payload: true });
        }
        else {
            await dispatch({ type: 'AUTH_DO', payload: false });
        }
      } catch (e) {
        await dispatch({ type: 'AUTH_DO', payload: false });
      }
    }
    request();
  }
}
