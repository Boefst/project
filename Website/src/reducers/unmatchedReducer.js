export default function reducer(state = {
    fetching: false,
    fetched: false,
    searched: false,
    error: null,
    infoMessage: null,
    data: null,
    oldData: null,
    invoices: [],
    payments: [],
    invoicesChanged: [],
    paymentsChanged: [],
    selectedInvoice: null,
    selectedPayment: null
}, action) {

    switch (action.type) {

        case 'FETCH_UNMATCHED': {
            return { ...state, fetching: true }
        }
        case 'FETCH_UNMATCHED_REJECTED': {
            return { ...state, fetching: false, error: action.payload }
        }
        case 'FETCH_UNMATCHED_FULLFILLED': {
            return {...state, fetching: false, fetched: true }
        }
        case 'ADD_UNMATCHED_PAYMENTS': {
            return { ...state, payments: action.payload, paymentsChanged: action.payload }
        }
        case 'ADD_UNMATCHED_INVOICES': {
            return { ...state, invoices: action.payload, invoicesChanged: action.payload }
        }
        case 'RESET_UNMATCHED_PAYMENTS': {
            return { ...state, paymentsChanged: state.payments }
        }
        case 'RESET_UNMATCHED_INVOICES': {
            return { ...state, invoicesChanged: state.invoices }
        }
        case 'SET_SELECTED_PAYMENT': {
            return { ...state, selectedPayment: action.payload }
        }
        case 'SET_SELECTED_INVOICE': {
            return { ...state, selectedInvoice: action.payload }
        }
        case 'SORT_PAYMENTS': {
            return { ...state, paymentsChanged: action.payload }
        }
        case 'SORT_INVOICES': {
            return { ...state, invoicesChanged: action.payload }
        }
        case 'SEARCH_PAYMENTS': {
            return { ...state, paymentsChanged: action.payload }
        }
        case 'SEARCH_INVOICES': {
            return { ...state, invoicesChanged: action.payload }
        }
        default: {
            return { ...state, error: 'default' }
        }
    }
}
