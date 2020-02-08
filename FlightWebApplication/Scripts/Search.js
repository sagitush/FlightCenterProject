//Shoots ajax to get information to fill the dropups.
$(document).ready(() => {
    $listAirlines = $("#listAirlines")
    $sourceCountryList = $("#sourceCountryList")
    $destinationCountryList = $("#destinationCountryList")
    $flightNumberList = $("#flightNumberList")
    $.ajax({
        url: "/api/anonymousFacade/flightDepartureAndLandingView"
    })
        .then((flightsWebapi) => {
            console.log(flightsWebapi)
            $.each(flightsWebapi, (i, namewebapi) => {
                const airlineName = namewebapi.AirlineCompanyName
                const originCounName = namewebapi.OriginCountryName
                const destCounName = namewebapi.DestinationCountryName
                const id = namewebapi.FlightNumber
                
                $listAirlines.append(
                    " <option>" + airlineName + "</option>")
                $sourceCountryList.append(
                    " <option>" + originCounName + "</option>")
                $destinationCountryList.append(
                    " <option>" + destCounName + "</option>")
                $flightNumberList.append(
                    " <option>" + id + "</option>")
            })
        })
        .catch((err) => { console.log(err) })
})
 
let airline = ''
console.log(airline)
let orgCount = ''
let destCount = ''
console.log(destCount)
let flightNum = 0
console.log(flightNum)

let searchT = ''

//Function that happends when you press on the "start searching" button.
//The function get the select item that the customer chose and pass it to the "searchFlights.html"
function findme() {

    airline = $('#listAirlines').val()
    console.log(airline)
    if (airline === '') {
        airline=""
    }

    orgCount = $('#sourceCountryList').val()
    console.log(orgCount)
    if (orgCount === '') {
        orgCount = ""
    }

    destCount = $('#destinationCountryList').val()
    console.log(destCount)
    if (destCount === '') {
        destCount = ""
    }

    flightNum = $('#flightNumberList').val()
    console.log(flightNum)
    if (flightNum === 0) {
        orgCount = 0
    }
    searchT = $('#searchType').val()
    if (searchT == '' || searchT == "Both") {
        window.location = "page/searchFlights?airline=" + airline + "&orgCoun=" + orgCount + "&destCoun=" + destCount + "&flightNum=" + flightNum
    }
    else {
        window.location = "page/searchFlights?airline=" + airline + "&orgCoun=" + orgCount + "&destCoun=" + destCount + "&flightNum=" + flightNum
            + "&searchType=" + searchT
    }

}


