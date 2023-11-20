import {
  defaultUserIconPath,
  emptyString,
  getRandomNumber,
  userIcons,
} from '@app/utils/utils';

export class Person {
  public id: number;
  public name: string;
  public lastName: string;
  public completeName: string;
  public dniType: string;
  public dni: string;
  public email: string;
  public completeIdentification: string;
  public creationDate: string = emptyString;
  public userImage: string;

  constructor(props: any) {
    this.id = props.id ?? 0;
    this.name = props.name ?? emptyString;
    this.lastName = props.lastName ?? emptyString;
    this.completeName = props.completeName ?? emptyString;
    this.dniType = props.dniType ?? emptyString;
    this.dni = props.dni.toString() ?? emptyString;
    this.completeIdentification = props.completeIdentification ?? emptyString;
    this.email = props.email ?? emptyString;
    this.userImage = userIcons[getRandomNumber(1, 7)] || defaultUserIconPath;

    if (props.creationDate) {
      const unprocessedDate = props.creationDate.slice(0, 19).replace('T', ' ');

      this.creationDate = new Date(unprocessedDate).toLocaleDateString();
    }
  }
}
