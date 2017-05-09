
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
   //add product to cart
   $(".addtocart").submit(function() {
        var url="/cart/addtocart"
        var postdata={productId:$(this).children(".pid").val()}
        $.post(url, postdata, function(data){
                $("#cartnumber").text(data.number)              
            },'json')
        event.preventDefault();
    })
    //add product to wishlist
   $(".addtolist").submit(function() {
        var url="/wishlist/addtowishlist"
        var postdata={productId:$(this).children(".pid").val()}
        var thiscell=$(this).parent()
        $.post(url, postdata, function(data){
                if(data.result=="success"){
                    var newhtml="<div class=\"result\"><p class=\"error\">Added to wishlist</p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a href=\"/wishlist/content\">View Wishlist</a></div>"
                } else if(data.result=="exist"){
                    var newhtml="<div class=\"result\"><p class=\"error\">Already in wishlist</p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a href=\"/wishlist/content\">View Wishlist</a></div>"
                }
                var resultdiv=thiscell.children(".result")
                if(resultdiv!=null){
                    resultdiv.remove()
                }  
                thiscell.append(newhtml)          
            },'json')
        event.preventDefault();
    })
    //add product in wishlist to cart
   $(".addwishlisttocart").submit(function() {
        var url="/cart/addtocart"
        var postdata={productId:$(this).children(".pid").val()}
        var thiscell=$(this).parent()
        var newcontent="<h3>Added to Cart</h3><form action=\"/orders\" method=\"post\"><input type=\"submit\" value=\"Proceed to checkout\"></form><form action=\"/cartcontent\" method=\"get\"><input type=\"submit\" value=\"View Cart\"></form>"
        $.post(url, postdata, function(data){
               thiscell.empty()
               thiscell.append(newcontent)              
            },'json')
        event.preventDefault();
    })
    //delete product in cart
   $(".deletecartitem").submit(function() {
        var url="/cart/deletecartitem"
        var postdata={productId:$(this).children(".pid").val()}
        var thisform=$(this)
        $.post(url, postdata, function(data){
            if(data.result=="success"){
                    thisform.closest('tr').remove();
                    // thisform.parent().parent().remove();
                    $("#quantity").text(data.quantity)
                    $("#total").text(data.total/100)                
                }
            },'json')
        event.preventDefault();
    })
    //delete product in wishlist
   $(".deletewishlistitem").submit(function() {
        var url="/wishlist/deletewishlistitem"
        var postdata={productId:$(this).children(".pid").val()}
        var thisform=$(this)
        $.post(url, postdata, function(data){
            if(data.result=="success"){
                    thisform.closest('tr').remove();
                    // thisform.parent().parent().remove();              
                }
            },'json')
        event.preventDefault();
    })
    //change the quantity of product in cart
   $(".changequantity").change(function() {
        var max = parseInt($(this).attr('max'));
        var min = parseInt($(this).attr('min'));
        if ($(this).val() > max)
        {
            $(this).val(max);
        }
        else if ($(this).val() < min)
        {
            $(this).val(min);
        }       
        var url="/cart/changequantity"
        var pid=$(this).parent().children(".pid").val()
        var newquantity=$(this).val()
        var postdata={productId:pid,quantity:newquantity}
        $.post(url, postdata, function(data){
            if(data.result=="success"){
                $("#quantity").text(data.quantity)
                $("#total").text(data.total/100)
            }
        },'json')
        // return false ;
        event.preventDefault();
    })
    //change the quantity of product in cart
   $(".changequantity").keypress(function(e) {
       var key=e.which
       if(key==13)
       {
           var max = parseInt($(this).attr('max'));
            var min = parseInt($(this).attr('min'));
            if ($(this).val() > max)
            {
                $(this).val(max);
            }
            else if ($(this).val() < min)
            {
                $(this).val(min);
            }       
            var url="/cart/changequantity"
            var pid=$(this).parent().children(".pid").val()
            var newquantity=$(this).val()
            var postdata={productId:pid,quantity:newquantity}
            $.post(url, postdata, function(data){
                if(data.result=="success"){
                    $("#quantity").text(data.quantity)
                    $("#total").text(data.total/100)
                }
            },'json')
            // return false ;
            event.preventDefault();
        }
    })

})

