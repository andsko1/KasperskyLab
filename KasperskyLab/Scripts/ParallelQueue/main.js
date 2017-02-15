function ParallelQueueBegin() {
	$('input', '#parallel-queue-result-form').prop('disabled', true);
	$('button[type="submit"]', '#parallel-queue-result-form').prop('disabled', true);
	var myDiv = document.createElement("div");
	myDiv.className = "alert alert-warning col-xs-12 col-sm-12 col-md-12 col-lg-12";
	myDiv.innerHTML = "<strong>Загрузка...";
	$('#parallel-queue-result-body').empty();
	$('#parallel-queue-result-body').append(myDiv);
}
function ParallelQueueFailure(request, error) {
	var myDiv = document.createElement("div");
	myDiv.className = "alert alert-danger col-xs-12 col-sm-12 col-md-12 col-lg-12";
	myDiv.innerHTML = "<strong>Ошибка";
	$('#parallel-queue-result-body').empty();
	$('#parallel-queue-result-body').append(myDiv);
	$('input', '#parallel-queue-result-form').prop('disabled', false);
	$('button[type="submit"]', '#parallel-queue-result-form').prop('disabled', false);
}
function ParallelQueueSuccess(result) {
	if (result.ok) {
		$('#parallel-queue-result-body').empty();
		$('#parallel-queue-result-body').html(result.resultHtml);
	}
	else {
		var myDiv = document.createElement("div");
		myDiv.className = "alert alert-danger col-xs-12 col-sm-12 col-md-12 col-lg-12";
		myDiv.innerHTML = "<strong>" + result.errormessage;
		$('#parallel-queue-result-body').empty();
		$('#parallel-queue-result-body').append(myDiv);
	}
	$('input', '#parallel-queue-result-form').prop('disabled', false);
	$('button[type="submit"]', '#parallel-queue-result-form').prop('disabled', false);
}