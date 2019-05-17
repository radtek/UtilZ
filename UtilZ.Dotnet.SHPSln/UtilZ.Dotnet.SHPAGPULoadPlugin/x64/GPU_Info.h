//*****************************************************************************
// �汾�ţ�GPU_Info V20180423
// �汾˵�����ö�̬�����ڲ�ѯGPU�������Ϣ
// ע�⣺��װ���Կ������汾���ܵ��ڸÿ�ʹ�õ�NVML��İ汾
//*******************************************************************************

#pragma once

#ifdef __cplusplus
extern "C" {
#endif

//******************************************************************************
// ���ܣ���ѯGPU����
// ���룺��
// �����n-GPU����
// ����ֵ��true-���óɹ���false-����ʧ��
__declspec(dllimport) bool GPUGetCount(unsigned int *n);

//******************************************************************************
// ���ܣ���ѯĳ��GPU�ڴ���Ϣ
// ���룺devID-�豸��
// �����totalMem-�ܵ��ڴ棻freeMem-ʣ���ڴ棻usedMem-��ʹ���ڴ档��λ��Ϊbyte��
// ����ֵ��true-���óɹ���false-����ʧ��
__declspec(dllimport) bool GPUGetMemInfo(unsigned int devID, unsigned long long *totalMem, unsigned long long *freeMem, unsigned long long *usedMem);

//******************************************************************************
// ���ܣ���ѯĳ��GPU������
// ���룺devID-�豸��
// �����utilizationRate-�����ʣ����ٷֱȱ�ʾ���磺utilizationRate=50����������Ϊ50%��
// ����ֵ��true-���óɹ���false-����ʧ��
__declspec(dllimport) bool GPUGetUtilizationRate(unsigned int devID, unsigned int *utilizationRate);

#ifdef __cplusplus
}
#endif