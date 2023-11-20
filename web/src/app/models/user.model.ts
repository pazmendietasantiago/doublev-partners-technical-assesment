import { emptyString } from '@app/utils/utils';

export class User {
  public id: number = 0;
  public user: string = emptyString;
  public user1: string = emptyString;
  public password: string = emptyString;
  public creationDate: string = emptyString;

  constructor(props: any) {
    this.id = props.id ?? 0;
    (this.user = props.user ?? emptyString),
      (this.user1 = props.user1 ?? emptyString),
      (this.password = props.password ?? emptyString);

    if (props.creationDate) {
      const unprocessedDate = props.creationDate.slice(0, 19).replace('T', ' ');

      this.creationDate = new Date(unprocessedDate).toLocaleDateString();
    }
  }
}
