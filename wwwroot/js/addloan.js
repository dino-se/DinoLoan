document.getElementById('addLoan').addEventListener('submit', function(event) {
    event.preventDefault();
    const formData = new FormData(this);
    formData.append('Status', 'None');
    fetch('../../loanapi/createloan', {
        method: 'POST',
        body: formData
    })
    .then(response => response.json())
    .then(() => {
        document.getElementById("addLoan").reset();
        location.reload();
    });
});