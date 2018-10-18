import React, { Component } from 'react'
import { connect } from 'react-redux'

class UnmatchedInvoices extends Component {
    constructor(props) {
        super()
        this.state = {
          buttonNumber: 1,
          order: "DESC",
          searched: false,
          searchData: "",
        }
        this.selectInvoice = this.selectInvoice.bind(this);
        this.changeButton = this.changeButton.bind(this);
        this.sortSum = this.sortSum.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.clearSearch = this.clearSearch.bind(this);
    }

    changeButton(number){
      this.setState({buttonNumber: number})
    }

    async selectInvoice(nr){
    }

    async sortSum(key, type){
      var orderTemp;
      if(this.state.order === "desc"){
        this.setState({order: "asc"})
        orderTemp = true;
      }else{
        this.setState({order: "desc"})
        orderTemp = false;
      }
    }

    async handleSubmit(event){
      event.preventDefault();
    }

    async handleChange(event){
      let change = {}
      change[event.target.name] = event.target.value;
      this.setState(change);
    }

    async clearSearch(event){
      let change = {}
      change["searchData"] = "";
      this.setState(change);
    }

    render() {
      return (
        <div className="unmatchedTable">
          <h2 className="wrapper-h2">Fakturor</h2>
          <form className="formSearch centeredSearch" onSubmit={this.handleSubmit}>
              <input id="search" className="adminSearchInput" type="text" name="searchData" onChange={this.handleChange} value={this.state.searchData} placeholder="Search" />
              <input type="submit" style={{ visibility: 'hidden', position: 'absolute' }} />
              <div className="adminSearchButton pointer asbG bgcAccent" onClick={this.handleSubmit}><i className="fas fa-search"></i></div>
              <div name="clear" className="adminSearchButton pointer asbO bgcOrange" onClick={this.clearSearch}><i className="fas fa-times"></i></div>
          </form>
          <div className="scrollTable">
            <table>
              <thead>
                <tr>
                  <th onClick={() => {this.sortSum("DocNr", true)}}>FaktNr<i className="fas fa-sort"></i></th>
                  <th onClick={() => {this.sortSum("OCR", true)}}>OCR<i className="fas fa-sort"></i></th>
                  <th onClick={() => {this.sortSum("Kund", true)}}>KundNr<i className="fas fa-sort"></i></th>
                  <th onClick={() => {this.sortSum("Name", false)}}>Namn<i className="fas fa-sort"></i></th>
                  <th onClick={() => {this.sortSum("InvDate", false)}}>FaktDatum<i className="fas fa-sort"></i></th>
                  <th onClick={() => {this.sortSum("DueDate", false)}}>FörfDatum<i className="fas fa-sort"></i></th>
                  <th onClick={() => {this.sortSum("Total", true)}}>Total<i className="fas fa-sort"></i></th>
                </tr>
              </thead>
              <tbody>
                {this.props.invoices.length > 0 && this.props.invoices.map(function(invoice){
                  return (
                    <tr className={this.state.buttonNumber === invoice.DocNr ? "matchRow matchRowActive" : "matchRow"} onClick={() => {this.selectInvoice(invoice.DocNr); this.changeButton(invoice.DocNr)} } key={invoice.DocNr}>
                      <td>{invoice.DocNr}</td>
                      <td>{invoice.OCR}</td>
                      <td>{invoice.Kund}</td>
                      <td>{invoice.Name}</td>
                      <td>{invoice.InvDate}</td>
                      <td>{invoice.DueDate}</td>
                      <td>{invoice.Total}</td>
                    </tr>
                  );
                }.bind(this))}
              </tbody>
            </table>
          </div>
        </div>
      );
    }
}

export default connect((store) => {
    return {
        invoices: store.manual.invoicesChanged
    }
})(UnmatchedInvoices);
