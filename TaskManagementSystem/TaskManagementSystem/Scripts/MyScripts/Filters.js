
function jsonDate() {
  
    return function (input, format) {
        if (angular.isUndefined(input)) return;
        var date = new Date(parseInt(input.substr(6))); // first 6 character is the date   
        if (angular.isUndefined(format)) format = "MM/DD/YYYY"; // default date format

        format = format.replace("DD", (date.getDate() < 10 ? '0' : '') + date.getDate()); // Pad with '0' if needed
        format = format.replace("MM", (date.getMonth() < 9 ? '0' : '') + (date.getMonth() + 1)); // Months are zero-based
        format = format.replace("YYYY", date.getFullYear());

        return format;
    };

}

function pagination() {
   
    return function (input, start) {
        if (!input || !input.length) { return; }
        start = parseInt(start, 10);
        return input.slice(start);
    };

}


jsonDateConvert.$inject = ['$filter'];
function jsonDateConvert($filter) {
    //MyApp.filter('jsonDateConvert', ['$filter', function ($filter) {
    return function (input, format) {
        return (input) ? $filter('date')(parseInt(input.substr(6)), format) : '';
    };
}