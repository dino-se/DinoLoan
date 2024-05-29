function getPayment(lid, total, pid) {
  document.getElementById("loanId").value = lid;
  document.getElementById("payId").value = pid;
  document.getElementById("currentAmmount").value = total;
  document.getElementById("totalPayments").innerHTML = total;
}

const totalPaymentsElement = document.getElementById("totalPayments");
const remainingBalanceElement = document.getElementById("remainingPayments");
const amountInput = document.getElementById("amount");

function updateRemainingBalance() {
  const amountPaid = parseFloat(totalPaymentsElement.textContent);
  const amountToPay = parseFloat(amountInput.value) || 0;
  const remainingBalance = amountPaid - amountToPay;
  remainingBalanceElement.textContent = remainingBalance.toFixed(2);
}

amountInput.addEventListener("input", updateRemainingBalance);