import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async getKeepById(id) {
    // This was for the activeKeep, but I don't think I'm using it.
    const found = AppState.keeps.find(k => k.id.toString() === id)
    if (found) {
      AppState.activeKeep = found
      return
    }
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
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
  }
}

export const keepsService = new KeepsService()
