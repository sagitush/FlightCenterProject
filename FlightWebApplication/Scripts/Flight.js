//This is the java script class for flight and it returns the status of the landing flights by the landing time.
class Flight {
 
    constructor(id, airlineid, origincountry, destcountry, departureTime, landingTime, status) {
        this.id = id
        this.airlineid = airlineid
        this.origincountry = origincountry
        this.destcountry = destcountry
        this.departureTime = departureTime
        this.landingTime = landingTime
        let presentDate = new Date()
        console.log(presentDate)
        let subtract120min = new Date(this.landingTime);
        console.log(subtract120min)
        let subtract15min = new Date(this.landingTime);

        subtract15min.setTime(subtract15min - 15 * 60 * 1000);
        console.log(subtract15min)
        subtract120min.setTime(subtract120min - 120 * 60 * 1000);
        console.log(subtract120min)
        let getvalidDate = function (d) { return new Date(d) }


        function validateDateBetweenTwoDates(fromDate, toDate, givenDate) {
            return getvalidDate(givenDate) <= getvalidDate(toDate) && getvalidDate(givenDate) >= getvalidDate(fromDate);
        }
        function afterThePlaneLanded(presentDate, LandTime) {
            return getvalidDate(LandTime) <= getvalidDate(presentDate);
        }
        if (validateDateBetweenTwoDates(subtract15min, this.landingTime, presentDate)) {

            this.status = "LANDING"
        }
        else if (afterThePlaneLanded(presentDate, this.landingTime)) {
            this.status = "LANDED"
        }
        else if (validateDateBetweenTwoDates(subtract120min, subtract15min, presentDate)) {
            this.status = "FINAL"
        }
        else {
            this.status = "NOT FINAL"
        }

    }
}