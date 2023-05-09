export class Player {
  playerId: string
  name: string
  gameCount: number
  averageAttempts: number

  constructor(playerId: string, name: string, gameCount: number, averageAttempts: number) {
    this.playerId = playerId
    this.name = name
    this.gameCount = gameCount
    this.averageAttempts = averageAttempts
  }
}
