// Write your Javascript code.
$(document).ready(function(){
    $(".item").click(function(){
        var targetID = $(this).children("img").attr("alt");
         window.location.href=`/show/product/${targetID}`;
    })

var currentId = $("#currentPageId").val();
console.log(currentId);

var listItems = $(".nav li");
listItems.each(function(li) {
    console.log($(this).attr('id'));
    if($(this).attr('id') == currentId && currentId !=null)
    {
        console.log('should be one');
        $(this).children('a').css('text-decoration', 'underline');
        $(this).children('a').css('color', 'pink');
    }
    
});

})