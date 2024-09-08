$(document).ready(function () {
    function CancelOrder(code) {
        var serviceUrl = api +"/Order";

        var urlWithParams = serviceUrl + "?productCode=" + encodeURIComponent(code) + "&customerEmail=" + encodeURIComponent(localStorage.getItem("userEmail"));
        $.ajax({
            url: urlWithParams,
            type: "DELETE",
            contentType: "application/json",
            success: function () {
                location.reload();
            },
            error: function () {
                alert("Failed to cancel order.");
            }
        });
    }

    $("#user-email").text(localStorage.getItem("userEmail"));
    var serviceUrl = api +"/Order";
    $.ajax({
        url: serviceUrl + "?customerEmail=" + encodeURIComponent(localStorage.getItem("userEmail")),
        type: "GET",
        crossDomain: true,
        success: function (data) {
            // Loop through each product in the response
            $.each(data, function (index, product) {
                // Create HTML for each product row
                var productRow = `
                          
                          <div class="p-4 border">${product.name}</div>
                          <div class="p-4 border">${product.description}</div>
                          <div class="p-4 border">${product.orderDate}</div>
                          <div class="p-4 border"><button class="cancel-btn bg-red-500 text-white px-4 py-2 rounded" data-product-code="${product.code}">Cancel</button></div>
                        `;
                // Append the product row to the grid
                $("#product-grid").append(productRow);
            });

            // Attach click event to Buy buttons
            $(".cancel-btn").click(function () {
                var productCode = $(this).data("product-code");
                CancelOrder(productCode)


            });
        },
        error: function () {
            alert("Failed to load products from the service.");
        }
    });
});