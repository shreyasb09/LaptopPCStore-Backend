$(document).ready(function(){
    console.log("HELLO");
    $("#purchase_date").focus( function() {
	    $(this).attr({type: 'datetime-local'});
      });
});