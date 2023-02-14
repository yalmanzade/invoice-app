// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function AddRow() {
    let productTemplate = document.getElementById("product-template");
    let productTarget = document.getElementById("product-target");
    productTarget.innerHTML += productTemplate.innerHTML;
}
function CreateInvoice() {
    var form = document.getElementById("form-data");
    var formData = document.forms['form-data'].elements['itemList[]'].value;
    console.log(formData);
}