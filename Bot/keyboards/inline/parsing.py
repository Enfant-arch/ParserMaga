from aiogram.types import InlineKeyboardMarkup, InlineQuery
from aiogram.types import InlineKeyboardButton



your_offers = InlineKeyboardButton("🔍 По названию", callback_data='my_buy')
referal_program = InlineKeyboardButton("МЕГА-КЕШБЕК", callback_data="my_licence")
promo_method = InlineKeyboardButton("", callback_data="promoBy")
toHome = InlineKeyboardButton("⬅ На главную", callback_data="Home")
UpBalance = InlineKeyboardButton("💸Пополнить баланс", callback_data='upBalance')
profile_InlineBoard = InlineKeyboardMarkup(row_width=1).add(your_offers, UpBalance, promo_method, toHome)