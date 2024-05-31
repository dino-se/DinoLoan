function calculateAmounts() {
    const amount = parseFloat(document.getElementById('amount').value) || 0;
    const interest = parseFloat(document.getElementById('interest').value) || 0;
    const deduction = parseFloat(document.getElementById('deduction').value) || 0;

    const interestAmount = (amount * interest) / 100;
    const bringHomeAmount = amount - deduction;

    document.getElementById('interestedAmount').value = interestAmount.toFixed(2);
    document.getElementById('bringHomeAmount').value = bringHomeAmount.toFixed(2);
}

document.getElementById('amount').addEventListener('input', calculateAmounts);
document.getElementById('interest').addEventListener('input', calculateAmounts);
document.getElementById('deduction').addEventListener('input', calculateAmounts);


async function ViewTransaction(itemId) {
    try {
        const response = await fetch(`/Loan/ViewTransaction?id=${itemId}`);
        if (response.ok) {
            const data = await response.text();
            document.getElementById('modalList').innerHTML = data;
        } else {
            console.error('Failed to fetch data');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}