$(document).ready(function($) {

	$(document).ready(function(){
		$(".ant-alert").fadeOut(4000);
	});

	function Print() {
		window.print();
	}
});

function AddItem(btn) {
    var table = document.getElementById("ExpTable");
    var rows = table.getElementsByTagName('tr');
    var rowOuterHtml = rows[rows.length - 1].outerHTML;
    var newRow = table.insertRow();
    newRow.innerHtml = rowOuterHtml;
}