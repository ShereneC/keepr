import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async getKeepById(id) {
    const found = AppState.keeps.find(k => k.id.toString() === id)
    if (found) {
      AppState.activeKeep = found
      AppState.activeKeep.views = AppState.activeKeep.views + 1
      // console.log('hello from getKeepById KeepsService', AppState.activeKeep) This was so helpful!!
      return
    }
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
    AppState.activeKeep.views++
  }

  async getKeepsByProfileId(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.keeps = res.data
  }

  async createKeep(keep) {
    const res = await api.post('/api/keeps', keep)
    AppState.keeps.push(res.data)
  }

  async deleteKeep(id) {
    await api.delete('api/keeps/' + id)
    keepsService.getAllKeeps()
  }

  async addKeepToVault(rawVaultKeep) {
    const res = await api.post('api/vaultkeeps', rawVaultKeep)
    AppState.vaultKeep = res.data
    // should I call get all vaults here or something? Then should I push them to the vault details page?
    // REVIEW to increase keep count, I think i should somehow filter through the vaultkeeps, select the ones that have a keepId of _______, but then how do I send that data back?
  }

  async removeKeepFromVault(id) {
    await api.delete('api/vaultkeeps/' + id)
    AppState.vaultkeeps = AppState.vaultkeeps.filter(vaultkeep => vaultkeep.id !== id)
    // Proper way to do this is by using a filter on the Appstate!!!!  So I'v done, it but it is still not updating it on the page.
    // REVIEW do i need to call getallkeeps?  I would have to pass in a profile id as well.  I got the count to show somehow.
  }

  // async increaseKeepCount(keepId) {
  //   await api.get('api/keeps/' + keepId)
  //   console.log('hello from increaseeKeepCount KeepsService', AppState.activeKeep)
  // um, okay, how do you edit the keep when the path in the server to edit keeps is [authorize]? Maybe you could go through get, but you've already got that path doing the regular getbyid
  // }

  async editTask(newTask, taskId) {
    const res = await api.put('api/tasks/' + taskId, newTask)
    return res.data.sprintId
  }
}

export const keepsService = new KeepsService()
