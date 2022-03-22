const moneyQuote = document.querySelector('#moneyQuote');

const quoteArray = [
    'The safest way to double your money is to fold it over and put it in your pocket.',
    'Too many people spend money they earned..to buy things they don\'t want..to impress people that they don\'t like.',
    'A bank is a place that will lend you money if you can prove that you don’t need it.',
    'Annual income twenty pounds, annual expenditure nineteen six, result happiness. Annual income twenty pounds, annual expenditure twenty pound ought and six, result misery.',
    'Money can\'t buy Happiness but it\'s more comfortable to cry in a Mercedes than on a bicycle.',
    'If you think nobody cares if you’re alive, try missing a couple of car payments.',
    'Inflation is when you pay fifteen dollars for the ten-dollar haircut you used to get for five dollars when you had hair.',
    'Money can’t buy happiness, but it can buy you the kind of misery you prefer.',
    'In this country, you gotta make the money first. Then when you get the money, you get the power. Then when you get the power, then you get the women.'
];

function randomItemFromArray(arr, not) {
    const item = arr[Math.floor(Math.random() * arr.length)];
    if (item === not) {
        return randomItemFromArray(arr, not);
    }
    return item;
}

moneyQuote.textContent = randomItemFromArray(quoteArray);