
$(document).ready(function () {
    $('#file-upload').change(function () {
        var fileName = $(this).val().split('\\').pop();
        $('#file-selected').text(fileName ? fileName : 'No file selected');
    });
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
    $('#upload-button').click(function () {
        $('#loader-overlay').removeClass('hidden');
        $('#upload-status').text('Uploading Product').css('color', 'gray');
            var fileInput = $('#file-upload')[0];
            var file = fileInput.files[0];
            
            if (!file) {
                alert('Please select a file to upload.');
                return;
            }

            var formData = new FormData();
            formData.append('file', file);

            $.ajax({
                url: api + "/Product/bulk",
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function(response) {
        showToast();
                },
                error: function(xhr, status, error) {
                    alert('File upload failed. Please try again.');
                },
                
                complete: function () {
                    // Hide loader after upload completes
                    $('#loader-overlay').addClass('hidden');
                }
            });
        });
    });

