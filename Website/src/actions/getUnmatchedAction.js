import { apiAddress } from './apiName'

/**
 * [Gets all unmatched invoices.]
 * @return {[type]} [redux reducer]
 */
export function getUnmatched() {
    return function (dispatch) {
        dispatch({ type: 'FETCH_UNMATCHED' });
        const request = async () => {
            try {
                const response = await fetch(apiAddress + '/api/payments', {
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
                await dispatch({ type: 'FETCH_UNMATCHED_FULLFILLED' })
                if (json.code === 200) {
                    const invoices = json.data.Invoices;
                    await dispatch({ type: 'ADD_UNMATCHED_INVOICES', payload: invoices })
                    const payments = [];
                    for(var i = 0; i < json.data.Payments.length; i++){
                        const payment = {
                          Reference: json.data.Payments[i].Reference,
                          Service: json.data.Payments[i].Service,
                          SaleAmount: json.data.Payments[i].SaleAmount,
                          Date: json.data.Payments[i].Date
                        }
                        payments.push(payment);
                    }
                    await dispatch({ type: 'ADD_UNMATCHED_PAYMENTS', payload: payments })
                }
            } catch (e) {
                console.log(e);
            }
        }
        request();
    }
}



/**
 * [matches the unmatched invoices between invoiceservice and paymentservice]
 * @param  {[type]} ref [ref number]
 * @param  {[type]} nr  [invoice number]
 * @return {[type]}     [no return]
 */
export function manualMatch(ref, nr) {
    return function (dispatch) {
        const request = async () => {
            try {
                const response = await fetch(apiAddress + '/api/payments/update?PaymentReference=' + ref + '&InvoiceDocNum=' + nr, {
                    mode: 'cors',
                    headers: {
                        'content-type': 'application/json',
                        'Access-Control-Allow-Origin': '*',
                        'ClientID': sessionStorage.getItem('clientID'),
                        'ClientSecret': sessionStorage.getItem('clientSecret'),
                    },
                    method: 'PATCH',
                });
                const json = await response.json();
                if (json.code === 200) {
                  await dispatch({ type: 'SET_SELECTED_PAYMENT', payload: null });
                  await dispatch({ type: 'SET_SELECTED_INVOICE', payload: null });
                }
            } catch (e) {
                console.log(e);
            }
        }
        request();
    }
}
