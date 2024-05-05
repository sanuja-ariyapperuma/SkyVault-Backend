document.body.addEventListener("validationErrors", function(evt){
    alert(evt.detail.value);
})