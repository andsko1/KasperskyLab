function StackSearchBegin() {
	$('input', '#stack-search-result-form').prop('disabled', true);
	$('button[type="submit"]', '#stack-search-result-form').prop('disabled', true);
	var myDiv = document.createElement("div");
	myDiv.className = "alert alert-warning col-xs-12 col-sm-12 col-md-12 col-lg-12";
	myDiv.innerHTML = "<strong>Загрузка...";
	$('#stack-search-result-body').empty();
	$('#stack-search-result-body').append(myDiv);
}
function StackSearchFailure(request, error) {
	var myDiv = document.createElement("div");
	myDiv.className = "alert alert-danger col-xs-12 col-sm-12 col-md-12 col-lg-12";
	myDiv.innerHTML = "<strong>Ошибка";
	$('#stack-search-result-body').empty();
	$('#stack-search-result-body').append(myDiv);
	$('input', '#stack-search-result-form').prop('disabled', false);
	$('button[type="submit"]', '#stack-search-result-form').prop('disabled', false);
}
function StackSearchSuccess(result) {
	if (result.ok) {
		$('#stack-search-result-body').empty();
		$('#stack-search-result-body').html(result.resultHtml);
	}
	else {
		var myDiv = document.createElement("div");
		myDiv.className = "alert alert-danger col-xs-12 col-sm-12 col-md-12 col-lg-12";
		myDiv.innerHTML = "<strong>" + result.errormessage;
		$('#stack-search-result-body').empty();
		$('#stack-search-result-body').append(myDiv);
	}
	$('input', '#stack-search-result-form').prop('disabled', false);
	$('button[type="submit"]', '#stack-search-result-form').prop('disabled', false);
}
function randomInRange(min, max, precision) {
	if (precision == 0)
		return Math.round((Math.random() * (max - min) + min * 1));
	return Math.round((Math.random() * (max - min) + min * 1) * 10 * precision) / 10 * precision;
}
$(document).ready(function () {
	$('#gen-confirm').on('click', function () {
		var generatedNumbers = [];
		for (var i = 0; i < $('#gen-count').val() ; i++)
		{
			generatedNumbers.push(randomInRange($('#gen-min').val(), $('#gen-max').val(), $('#gen-precision').val()))
		}
		$('#Numbers').val(generatedNumbers.join());
	});
});