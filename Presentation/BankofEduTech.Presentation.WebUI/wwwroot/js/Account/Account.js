function createAccount() {

    var data = {

        AccountName: document.getElementById("accountName").value,
        Currency: document.getElementById("currencyType").value


    };
    console.log(data);
    $.ajax({
        type: "POST",
        url: "/User/Account/CreateAccount", 
        contentType: "application/json", 
        data: JSON.stringify(data), 
        success: function (result) {
            console.log(result);
            if (result.isSuccess == true) {
                Swal.fire({
                    title: "İşlem başarılı!",
                    text: "Hesap açma işlemi başarılı bir şekilde gerçekleştirildi!",
                    icon: "success"
                });


            }
            else {
                var error = "";
                result.messages.forEach(function (element) {
                    error += element + " <br>";
                });
                console.log(error);
                Swal.fire({
                    icon: "error",
                    title: "Hata...",
                    html: error

                });
            }

        },
        error: function (xhr, status, error) {
            console.error("Hata:", error);
            alert("Güncelleme işlemi başarısız oldu.");
        }
    });


}