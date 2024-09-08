    $(document).ready(function () {
       
            function showToast() {
                const toastContainer = document.getElementById('toast-container');
                toastContainer.classList.remove('hidden', 'toast-exit');
                toastContainer.classList.add('toast-enter');
                setTimeout(() => {
                    toastContainer.classList.remove('toast-enter');
                    toastContainer.classList.add('toast-exit');
                    setTimeout(() => {
                        toastContainer.classList.add('hidden');
                    }, 15000); // Match the duration of the exit animation
                }, 500); // Show toast for 3 seconds
            }
            function PlaceOrder(code) {
                var serviceUrl = api+"/Order";

                var urlWithParams = serviceUrl + "?productCode=" + encodeURIComponent(code) + "&customerEmail=" + encodeURIComponent(localStorage.getItem("userEmail"));
                $.ajax({
                    url: urlWithParams,
                    type: "POST",
                    contentType: "application/json",
                    success: function () {
                        showToast();
                    },
                    error: function () {
                        alert("Failed to place order.");
                    }
                });
            }

            $("#user-email").text(localStorage.getItem("userEmail"));

           var serviceUrl =  api +"/Product";
            $.ajax({
                url: serviceUrl,
                type: "GET",
                crossDomain: true,
                success: function (data) {
                    // Loop through each product in the response
                    $.each(data, function (index, product) {
                        // Create HTML for each product row
                        var productRow = `
                              <div class="p-4 border">${product.code}</div>
                              <div class="p-4 border">${product.name}</div>
                              <div class="p-4 border">${product.description}</div>
                              <div class="p-4 border"><button class="buy-btn bg-blue-500 text-white px-4 py-2 rounded" data-product-code="${product.code}">Buy</button></div>
                            `;
                        // Append the product row to the grid
                        $("#product-grid").append(productRow);
                    });

                    // Attach click event to Buy buttons
                    $(".buy-btn").click(function () {
                        var productCode = $(this).data("product-code");
                        PlaceOrder(productCode)


                    });
                },
                error: function () {
                    alert("Failed to load products from the service.");
                }
            });
        });