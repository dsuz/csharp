using UnityEngine;

/// <summary>
/// null �̐����Ƃ��āASprite �̐F��ς���B
/// </summary>
public class NullExample : MonoBehaviour
{
    /// <summary>�ς����̐F</summary>
    [SerializeField] Color _color = Color.red;

    /// <summary>
    /// Sprite �̐F��ς���B�O������Ăяo���Ďg���B
    /// </summary>
    public void ChangeColor()
    {
        // SpriteRenderer �R���|�[�l���g���擾����B�ǉ�����ĂȂ��ꍇ�� null ���߂����B
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _color;
    }

    /// <summary>
    /// Sprite �̐F��ς���B�O������Ăяo���Ďg���B
    /// �G���[�ɂȂ�Ȃ��悤�� null �`�F�b�N�����Ă���B
    /// </summary>
    public void ChangeColorWithNullCheck()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // null �`�F�b�N
        if (spriteRenderer != null)
        {
            // null ����Ȃ���ΐF��ς���
            spriteRenderer.color = _color;
        }
        else
        {
            // null ��������C���X�^���X�𑀍삹���A�x�����o�͂���
            Debug.LogWarning($"{name} �� Sprite Renderer �R���|�[�l���g���ǉ�����Ă��܂���");
        }
    }
}
