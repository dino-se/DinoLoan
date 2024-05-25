document.getElementById("birthdate").addEventListener("change", function () {
  var birthdate = new Date(this.value);
  var today = new Date();
  var nextBirthday = new Date(
    today.getFullYear(),
    birthdate.getMonth(),
    birthdate.getDate()
  );
  if (today > nextBirthday) {
    nextBirthday.setFullYear(nextBirthday.getFullYear() + 1);
  }
  var age = nextBirthday.getFullYear() - birthdate.getFullYear();
  document.getElementById("birthday").value = age;
});
