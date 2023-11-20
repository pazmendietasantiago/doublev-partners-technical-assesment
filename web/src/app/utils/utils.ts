export const emptyString: string = '';

export const defaultUserIconPath: string = 'assets/images/icons/user/user_1.png';

export const userIcons: any = {
  0: defaultUserIconPath,
  1: 'assets/images/icons/user/user_2.png',
  2: 'assets/images/icons/user/user_3.png',
  4: 'assets/images/icons/user/user_4.png',
  5: 'assets/images/icons/user/user_5.png',
  6: 'assets/images/icons/user/user_6.png',
  7: 'assets/images/icons/user/user_7.png',
  8: 'assets/images/icons/user/user_8.png',
};

export function getRandomNumber(minimun: number = 1, maximun: number = 100) {
    return Math.trunc(Math.random() * (maximun - minimun) + minimun);
}
